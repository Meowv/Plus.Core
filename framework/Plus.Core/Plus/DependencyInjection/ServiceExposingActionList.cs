using System;
using System.Collections.Generic;

namespace Plus.DependencyInjection
{
    public class ServiceExposingActionList : List<Action<IOnServiceExposingContext>>
    {

    }
}