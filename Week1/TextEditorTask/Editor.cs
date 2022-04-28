namespace Week1.TextEditorTask
{
    public class Editor
    {
        internal string text { get; set; }
        internal List<List<char>> TextBox { get; set; }
        internal int _row, _col;
        internal char currChar { get; set; }

        public Editor()
        {
            TextBox = new List<List<char>>();
        }

        public char CurrentCursor()
        {
            return TextBox[_row][_col];
        }

        public void AddText(string input)
        {
            text = input; 
            char[] chars = text.ToCharArray();
            List<char> result = new List<char>();
            for (int i = 0; i < chars.Length; i++)
            {
                result.Add(chars[i]);
            }
            TextBox.Add(result);
            Console.WriteLine($"ROW: {TextBox.Count - 1} | COL: {result.Count - 1}");
        }

        public void MoveCursorTo(int row, int col)
        {
            _row = row;
            _col = col;
            currChar = TextBox[row][col];
        }

        public void InsertChar(char c)
        {
            TextBox[_row].Insert(_col, c);
        }

        public void DeleteChar()
        {
            currChar = TextBox[_row][_col];
            TextBox[_row].RemoveAt(_col);
        }

        public void Display()
        {
            foreach (var item in TextBox)
            {
                foreach (char c in item)
                {
                    Console.Write(c);
                }
            }
        }
    }
}
