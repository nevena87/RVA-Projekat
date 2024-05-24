using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RVAProjekatDisco.Komande
{
    public class CommandExecutor
    {
        Stack<IUndoRedo> undoStack = new Stack<IUndoRedo>();
        Stack<IUndoRedo> redoStack = new Stack<IUndoRedo>();

        public void DodajIIzvrsi(IUndoRedo novaKomanda)
        {
            novaKomanda.Izvrsi();
            undoStack.Push(novaKomanda);
            redoStack.Clear();
        }

        public void Undo()
        {
            IUndoRedo undoCommand = undoStack.Pop();
            undoCommand.Vrati();
            redoStack.Push(undoCommand);
        }

        public bool ValidacijaUndo()
        {
            bool vrati = (undoStack.Count > 0) ? true : false;
            return vrati;
        }

        public void Redo()
        {
            IUndoRedo redoCommand = redoStack.Pop();
            redoCommand.Izvrsi();
            undoStack.Push(redoCommand);
        }

        public bool ValidacijaRedo()
        {
            bool vrati = (redoStack.Count > 0) ? true : false;
            return vrati;
        }
    }
}
