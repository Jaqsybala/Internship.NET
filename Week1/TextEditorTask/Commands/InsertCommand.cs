namespace Week1.TextEditorTask.Commands
{
    public class InsertCommand : ICommand
    {
        private Editor _editor;
        private char _char;

        public InsertCommand(Editor editor, char chars)
        { 
            _editor = editor;
            _char = chars;
        }

        public void Execute()
        {
            _editor.InsertChar(_char);
        }

        public void Redo()
        {
            _editor.InsertChar(_char);
        }

        public void Undo()
        {
            _editor.DeleteChar();
        }
    }
}
