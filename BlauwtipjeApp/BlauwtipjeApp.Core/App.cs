namespace BlauwtipjeApp.Core
{
    public static class App
    {
        private static INativeApp app;

        public static void Initialize(INativeApp app)
        {
            App.app = app;
        }

        public static void Shutdown()
        {
            App.app.Shutdown();
        }

        public interface INativeApp
        {
            void Shutdown();
        }
    }
}
