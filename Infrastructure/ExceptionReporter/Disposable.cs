using System;
using System.Threading;

namespace Infrastructure.ExceptionReporter
{
    /// <summary>
    /// 抽象析构类
    /// </summary>
    /// <seealso cref="System.IDisposable" />
	public abstract class Disposable : IDisposable
    {
        /// <summary>
        /// The disposed
        /// </summary>
        private int disposed;

        /// <summary>
        /// Gets a value indicating whether this instance is disposed.
        /// </summary>
        /// <value><c>true</c> if this instance is disposed; otherwise, <c>false</c>.</value>
        public bool IsDisposed
        {
            get
            {
                return this.disposed == 1;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Disposable"/> class.
        /// </summary>
        protected Disposable()
        {
            this.disposed = 0;
        }

        /// <summary>
        /// Disposes this instance.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (Interlocked.CompareExchange(ref this.disposed, 1, 0) == 0)
            {
                if (disposing)
                {
                    this.DisposeManagedResources();
                }
                this.DisposeUnmanagedResources();
            }
        }

        /// <summary>
        /// Disposes the managed resources.
        /// </summary>
        protected virtual void DisposeManagedResources()
        {
        }

        /// <summary>
        /// Disposes the unmanaged resources.
        /// </summary>
        protected virtual void DisposeUnmanagedResources()
        {
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="Disposable"/> class.
        /// </summary>
        ~Disposable()
        {
            this.Dispose(false);
        }
    }
}