using Week1.TextEditorTask;
using Xunit;

namespace UnitTests.Week1.TextEditorTask.Tests
{
    public class UndoCommandTests
    {
        [Theory]
        [InlineData(0, 1, 'H')]
        [InlineData(0, 2, 'e')]
        [InlineData(0, 3, 'l')]
        [InlineData(0, 4, 'l')]
        public void Undo_After_MoveToCursor(int row, int col, char expected)
        {
            Application app = new Application();

            app.AddText("Hello");
            app.MoveToCursor(row, col);
            app.Undo();

            Assert.Equal(expected, app.CurrentCursor());
        }

        [Theory]
        [InlineData(0, 0, 'H')]
        [InlineData(0, 1, 'e')]
        [InlineData(0, 2, 'l')]
        [InlineData(0, 3, 'l')]
        [InlineData(0, 4, 'o')]
        public void Undo_After_InsertChar(int row, int col, char expected)
        {
            Application app = new Application();

            app.AddText("Hello");
            app.MoveToCursor(row, col);
            app.InserChar('A');
            app.Undo();

            Assert.Equal(expected, app.CurrentCursor());
        }

        [Theory]
        [InlineData("Hello", 'l', 0)]
        [InlineData("Hello", 'l', 1)]
        [InlineData("Hello", 'e', 2)]
        [InlineData("Hello", 'H', 3)]
        public void Undo_After_DeleteChar(string input, char expected, int iterator)
        {
            Application app = new Application();

            app.AddText("Hello");
            for (int i = 0; i < (input.Length - 1) - iterator; i++)
            {
                app.DeleteChar();
            }
            app.Undo();

            Assert.Equal(expected, app.CurrentCursor());
        }
    }
}
