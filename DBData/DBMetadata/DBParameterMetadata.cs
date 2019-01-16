using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Data.DataModel;

namespace DBData.DBMetadata
{
    [Table("ParameterMetadata")]
    public class DBParameterMetadata : BaseParameterMetadata
    {
        #region Constructor

        public DBParameterMetadata()
        {
            MethodParameters = new HashSet<DBMethodMetadata>();
            TypeFields = new HashSet<DBTypeMetadata>();
        }

        #endregion

        #region Properties

        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public override string Name { get; set; }

        public new DBTypeMetadata Type { get; set; }

        #endregion

        #region Inverse Properties

        public virtual ICollection<DBMethodMetadata> MethodParameters { get; set; }

        public virtual ICollection<DBTypeMetadata> TypeFields { get; set; }

        #endregion

    }
}