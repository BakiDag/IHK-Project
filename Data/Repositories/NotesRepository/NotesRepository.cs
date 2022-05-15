using DataAccessEfCore.DataContext;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessEfCore.Repositories.NotesRepository
{
    public class NotesRepository : INotesRepository
    {
        private readonly WochenberichtDBContext _dbContext;

        public NotesRepository(WochenberichtDBContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<Note> CreateNoteAsync(Note _Notes)
        {
            var added = _dbContext.Notes.Add(_Notes) != null;
            if (added == false)
            {
                _Notes = null;
                return _Notes;
            }
            
            return _Notes;
        }

        public async Task<Note> DeleteNoteAsyncByID(int id)
        {
            var _note = _dbContext.Notes.Where(x => x.ID == id)
                                                    .FirstOrDefault();
            if (_note != null)
            {
                _dbContext.Notes.Remove(_note);
                return _note;
            }
            return _note;
        }

    }
}
