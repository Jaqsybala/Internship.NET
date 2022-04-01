namespace Week1.TextEditorTask.Commands
{
    public class MoveToCursorCommand : ICommand
    {
        private Editor _editor;
        private int _row;
        private int _column;
        private int _preRow;
        private int _preCol;

        public MoveToCursorCommand(Editor editor, int row, int column)
        { 
            _editor = editor;
            _row = row;
            _column = column;
            _preRow = row;
            _preCol = column - 1;
        }

        public void Execute()
        {
            _editor.MoveCursorTo(_row, _column);
        }

        public void Redo()
        {
            _editor.MoveCursorTo(_row, _column);
            Console.WriteLine($"Current cursor: {_editor.CurrentCursor()}");
        }

        public void Undo()
        {
            _editor.MoveCursorTo(_preRow, _preCol);
            Console.WriteLine($"Current cursor: {_editor.CurrentCursor()}");
        }
    }
}
