using System;
using System.Collections;
using System.Linq;
using Soneta.Business;
using Soneta.Kadry.Forms;
using Soneta.Types;
using Soneta.Zadania;

[assembly: Worker(typeof(DodajUrzadzenia.DodajUrzadzenia2Worker), typeof(Soneta.Zadania.Zadanie))]

namespace DodajUrzadzenia
{
    [Caption("Dodaj lub usuń urządzenia")]
    public class DodajUrzadzenia2Worker : DodajUsuńBase, IDisposable {

        [Context]
        public Zadanie Zlecenie { get; set; }

        [Action("Dodaj moje urządzenia",
            Target = ActionTarget.ToolbarWithText | ActionTarget.Menu,
            Icon = ActionIcon.Wizard,
            Mode = ActionMode.SingleSession | ActionMode.OnlyForm | ActionMode.NonAccept,
            Priority = 1)]
        public DodajUrzadzenia2Worker DodajUrzadzenia() => this;

        private View vElementy;
        public override IEnumerable Elementy {
            get
            {
                if (vElementy == null)
                {
                    vElementy = Zlecenie.Module.Urzadzenia.CreateView();
                }
                return vElementy;
            }
        }

        private View vWybrane;
        public override IEnumerable Wybrane
        {
            get {
                if (vWybrane == null)
                {
                    vWybrane = Zlecenie.UrzadzeniaUzyte.CreateView();
                    vWybrane.RowType = typeof(UrzadzenieUzyte);
                }
                return vWybrane;
            }
        }

        public override Row Dodaj(IEnumerable rows)
        {
            Row result = null;
            foreach (Urzadzenie urz in rows)
            {
                // Dodać każde urządzenie znajdujące się na liście rows do Zlecenie.UrzadzeniaUzyte - jeśli tam go jeszcze nie ma
            }
            return result;
        }

        public override Row Usuń(IEnumerable rows)
        {
            Row result = null;
            foreach (Urzadzenie urz in rows)
            {
                // Usunąć każde UrządzenieUzyte znajdujące się na liście rows ze Zlecenie.UrzadzeniaUzyte
            }
            return result;
        }

        #region Action Buttons

        public void Add()
        {
            using (ITransaction t = this.Session.Logout(true))
            {
                Dodaj(ElementyItem);
                t.CommitUI();
            }
        }

        public void Remove()
        {
            using (ITransaction t = this.Session.Logout(true))
            {
                Usuń(WybraneItem);
                t.CommitUI();
            }
        }

        public void AddAll()
        {
            using (ITransaction t = this.Session.Logout(true))
            {
                Dodaj(Elementy);
                t.CommitUI();
            }
        }

        public void RemoveAll()
        {
            using (ITransaction t = this.Session.Logout(true))
            {
                Usuń(Wybrane);
                t.CommitUI();
            }
        }

        #endregion
        public Urzadzenie[] ElementyItem { get; set; }
        public UrzadzenieUzyte[] WybraneItem { get; set; }

        #region IDisposable

        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    vWybrane = null;
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion IDisposable

    }
}
