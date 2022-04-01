using Week1.TextEditorTask;
using Xunit;

namespace UnitTests.Week1.TextEditorTask.Tests
{
    public class InsertCommandTests
    {
        [Theory]
        [InlineData(0, 0, 'M', 'M')]
        [InlineData(0, 1, 'V', 'V')]
        [InlineData(0, 2, 'G', 'G')]
        [InlineData(0, 3, 'P', 'P')]
        [InlineData(0, 4, 'S', 'S')]
        [InlineData(0, 5, 'A', 'A')]
        public void InsertCharTest(int row, int col, char input, char expected)
        {
            Application app = new Application();

            app.AddText("Hello!");
            app.MoveToCursor(row, col);
            app.InserChar(input);

            Assert.Equal(expected, app.CurrentCursor());
        }
    }
}
