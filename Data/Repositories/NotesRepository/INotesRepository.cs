using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessEfCore.Repositories.NotesRepository
{
    public interface INotesRepository
    {
        Task<Note> CreateNoteAsync(Note _Notes);

    }
}
