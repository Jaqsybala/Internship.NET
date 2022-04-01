using Week1.TextEditorTask;

namespace Week1.TextEditorTask
{
    public class TextEditor
    {
        static void Main(string[] args)
        {
            Application application = new Application();
            Console.WriteLine("Welcome to Simple Text Editor");
            while (true)
            {
                Console.WriteLine("\nPlease enter:\n1) A add to text\n2) M to move to cursor to particular character\n" +
                    "3) I to insert a character\n4) D to delete a character\n5) U to undo\n6) R to redo\n7) S to show");
                var op = Console.ReadLine();
                switch (op)
                {
                    case "A":
                        Console.WriteLine("Write the text");
                        string text = Console.ReadLine();
                        application.AddText(text);
                        Console.WriteLine($"Current cursor: {application.CurrentCursor()}");
                        break;
                    case "M":
                        Console.WriteLine("Give a row number");
                        int row = int.Parse(Console.ReadLine());
                        Console.WriteLine("Give a column number");
                        int col = int.Parse(Console.ReadLine());
                        application.MoveToCursor(row, col);
                        Console.WriteLine($"Current cursor: {application.CurrentCursor()}");
                        break;
                    case "I":
                        Console.WriteLine("Give a character");
                        char input = char.Parse(Console.ReadLine());
                        application.InserChar(input);
                        break;
                    case "D":
                        application.DeleteChar();
                        break;
                    case "U":
                        application.Undo();
                        break;
                    case "R":
                        application.Redo();
                        break;
                    case "S":
                        application.Display();
                        break;
                    default:
                        return;
                }    
            }
        }
    }
}