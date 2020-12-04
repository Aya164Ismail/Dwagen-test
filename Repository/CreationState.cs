using System;
using System.Collections.Generic;
using System.Text;

namespace Dwagen.Repository
{
    public class CreationState
    {
        public bool IsCreatedSuccessfully { get; set; }
        public Guid? CreatedObjectId { get; set; }
        public ICollection<string> ErrorMessages { get; set; } = new List<string>();
    }
}
