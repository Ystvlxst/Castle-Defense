public static class ModificationsContainer
{
    public static T Get<T>() where T : new()
    {
        if (ModificationHolder<T>.Instance == null)
            ModificationHolder<T>.Instance = new T();
        
        return ModificationHolder<T>.Instance;
    }

    private static class ModificationHolder<T>
    {
        public static T Instance;
    }
}