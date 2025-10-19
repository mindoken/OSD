namespace Application
{
    public sealed class SceneLoadStartCommand : ISceneLoadStartCommand
    {
        private readonly CursorService _cursorService;

        public SceneLoadStartCommand(CursorService cursorService)
        {
            _cursorService = cursorService;
        }

        void ISceneLoadStartCommand.Execute()
        {
            _cursorService.SetDefaultCursor();
        }
    }
}