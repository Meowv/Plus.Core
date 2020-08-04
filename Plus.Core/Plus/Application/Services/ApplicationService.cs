using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Plus.Aspects;
using Plus.Auditing;
using Plus.Authorization;
using Plus.DependencyInjection;
using Plus.Features;
using Plus.Guids;
using Plus.Linq;
using Plus.Localization;
using Plus.MultiTenancy;
using Plus.ObjectMapping;
using Plus.Settings;
using Plus.Timing;
using Plus.Uow;
using Plus.Users;
using Plus.Validation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Plus.Application.Services
{
    public abstract class ApplicationService :
        IApplicationService,
        IAvoidDuplicateCrossCuttingConcerns,
        IValidationEnabled,
        IUnitOfWorkEnabled,
        IAuditingEnabled,
        ITransientDependency
    {
        public IServiceProvider ServiceProvider { get; set; }
        protected readonly object ServiceProviderLock = new object();

        protected TService LazyGetRequiredService<TService>(ref TService reference)
            => LazyGetRequiredService(typeof(TService), ref reference);

        protected TRef LazyGetRequiredService<TRef>(Type serviceType, ref TRef reference)
        {
            if (reference == null)
            {
                lock (ServiceProviderLock)
                {
                    if (reference == null)
                    {
                        reference = (TRef)ServiceProvider.GetRequiredService(serviceType);
                    }
                }
            }

            return reference;
        }

        public static string[] CommonPostfixes { get; set; } = { "AppService", "ApplicationService", "Service" };

        public List<string> AppliedCrossCuttingConcerns { get; } = new List<string>();

        protected IUnitOfWorkManager UnitOfWorkManager => LazyGetRequiredService(ref _unitOfWorkManager);
        private IUnitOfWorkManager _unitOfWorkManager;

        protected IAsyncQueryableExecuter AsyncExecuter => LazyGetRequiredService(ref _asyncExecuter);
        private IAsyncQueryableExecuter _asyncExecuter;

        protected Type ObjectMapperContext { get; set; }
        protected IObjectMapper ObjectMapper
        {
            get
            {
                if (_objectMapper != null)
                {
                    return _objectMapper;
                }

                if (ObjectMapperContext == null)
                {
                    return LazyGetRequiredService(ref _objectMapper);
                }

                return LazyGetRequiredService(
                    typeof(IObjectMapper<>).MakeGenericType(ObjectMapperContext),
                    ref _objectMapper
                );
            }
        }
        private IObjectMapper _objectMapper;

        public IGuidGenerator GuidGenerator { get; set; }

        protected ILoggerFactory LoggerFactory => LazyGetRequiredService(ref _loggerFactory);
        private ILoggerFactory _loggerFactory;

        protected ICurrentTenant CurrentTenant => LazyGetRequiredService(ref _currentTenant);
        private ICurrentTenant _currentTenant;

        protected ICurrentUser CurrentUser => LazyGetRequiredService(ref _currentUser);
        private ICurrentUser _currentUser;

        protected ISettingProvider SettingProvider => LazyGetRequiredService(ref _settingProvider);
        private ISettingProvider _settingProvider;

        protected IClock Clock => LazyGetRequiredService(ref _clock);
        private IClock _clock;

        protected IAuthorizationService AuthorizationService => LazyGetRequiredService(ref _authorizationService);
        private IAuthorizationService _authorizationService;

        protected IFeatureChecker FeatureChecker => LazyGetRequiredService(ref _featureChecker);
        private IFeatureChecker _featureChecker;

        protected IStringLocalizerFactory StringLocalizerFactory => LazyGetRequiredService(ref _stringLocalizerFactory);
        private IStringLocalizerFactory _stringLocalizerFactory;

        protected IStringLocalizer L
        {
            get
            {
                if (_localizer == null)
                {
                    _localizer = CreateLocalizer();
                }

                return _localizer;
            }
        }
        private IStringLocalizer _localizer;

        protected Type LocalizationResource
        {
            get => _localizationResource;
            set
            {
                _localizationResource = value;
                _localizer = null;
            }
        }
        private Type _localizationResource = typeof(DefaultResource);

        protected IUnitOfWork CurrentUnitOfWork => UnitOfWorkManager?.Current;

        protected ILogger Logger => _lazyLogger.Value;
        private Lazy<ILogger> _lazyLogger => new Lazy<ILogger>(() => LoggerFactory?.CreateLogger(GetType().FullName) ?? NullLogger.Instance, true);

        protected ApplicationService()
        {
            GuidGenerator = SimpleGuidGenerator.Instance;
        }

        /// <summary>
        /// Checks for given <paramref name="policyName"/>.
        /// Throws <see cref="PlusAuthorizationException"/> if given policy has not been granted.
        /// </summary>
        /// <param name="policyName">The policy name. This method does nothing if given <paramref name="policyName"/> is null or empty.</param>
        protected virtual async Task CheckPolicyAsync([CanBeNull] string policyName)
        {
            if (string.IsNullOrEmpty(policyName))
            {
                return;
            }

            await AuthorizationService.CheckAsync(policyName);
        }

        protected virtual IStringLocalizer CreateLocalizer()
        {
            if (LocalizationResource != null)
            {
                return StringLocalizerFactory.Create(LocalizationResource);
            }

            var localizer = StringLocalizerFactory.CreateDefaultOrNull();
            if (localizer == null)
            {
                throw new PlusException($"Set {nameof(LocalizationResource)} or define the default localization resource type (by configuring the {nameof(PlusLocalizationOptions)}.{nameof(PlusLocalizationOptions.DefaultResourceType)}) to be able to use the {nameof(L)} object!");
            }

            return localizer;
        }
    }
}
