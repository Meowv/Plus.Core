namespace Plus.VirtualFileSystem
{
    public class PlusVirtualFileSystemOptions
    {
        public VirtualFileSetList FileSets { get; }

        public PlusVirtualFileSystemOptions()
        {
            FileSets = new VirtualFileSetList();
        }
    }
}