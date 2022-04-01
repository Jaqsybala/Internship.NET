namespace Week1.TextEditorTask.Commands
{
    public class AddTextCommand : ICommand
    {
        private Editor _editor;
        private string _text;

        public AddTextCommand(Editor editor, string text)
        {
            _editor = editor;
            _text = text;
        }

        public void Execute()
        {
            _editor.AddText(_text);
        }

        public void Redo()
        {
            _editor.AddText(_text);
        }

        public void Undo()
        {
            for (int i = 0; i < _text.Length; i++)
            {
                _editor.DeleteChar();
            }
        }
    }
}
