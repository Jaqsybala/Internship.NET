namespace Week2.DisposeTask
{
    public class Customer : IDisposable
    {
        private StringReader _reader;
        private bool disposed = false;
        public bool Disposed
        {
            get { return disposed; }
        }

        public Customer()
        {
            this._reader = new StringReader("");
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        { 
            if (!disposed)
            {
                if (disposing)
                {
                    if (_reader != null)
                    { 
                        this._reader.Dispose();
                    }
                }
                disposed = true;
            }
        }

        ~Customer()
        { 
            Dispose(false); 
        }
    }
}
