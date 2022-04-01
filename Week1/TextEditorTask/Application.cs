using Week1.TextEditorTask.Commands;

namespace Week1.TextEditorTask
{
    public class Application
    {
        private Editor _editor;
        private Stack<ICommand> _undo;
        private Stack<ICommand> _redo;

        public Application()
        {
            _editor = new Editor();
            _undo = new Stack<ICommand>();
            _redo = new Stack<ICommand>();
        }

        public void AddText(string input)
        { 
            ClearRedo();
            AddTextCommand addTextCommand = new AddTextCommand(_editor, input);
            addTextCommand.Execute();
            _undo.Push(addTextCommand);
        }

        public void Display()
        { 
            _editor.Display();
        }

        public char CurrentCursor()
        {
            return _editor.CurrentCursor();
        }

        public void MoveToCursor(int row, int col)
        {
            ClearRedo();
            MoveToCursorCommand moveToCursor = new MoveToCursorCommand(_editor, row, col);
            moveToCursor.Execute();
            _undo.Push(moveToCursor);
        }

        public void InserChar(char input)
        {
            ClearRedo();
            InsertCommand insertCommand = new InsertCommand(_editor, input);
            insertCommand.Execute();
            _undo.Push(insertCommand);
        }

        public void DeleteChar()
        {
            ClearRedo();
            DeleteCommand deleteCommand = new DeleteCommand(_editor);
            deleteCommand.Execute();
            _undo.Push(deleteCommand);        
        }

        public void Undo()
        {
            if (_undo.Count != 0)
            {
                ICommand command = _undo.Pop();
                command.Undo();
                _redo.Push(command);
            }
        }

        public void Redo()
        {
            if (_redo.Count != 0)
            {
                ICommand command = _redo.Pop();
                command.Redo();
                _undo.Push(command);
            }
        }

        private void ClearRedo()
        {
            while (_redo.Count() != 0)
            {
                _redo.Pop();
            }
        }
    } 
}
