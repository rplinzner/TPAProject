using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Data.DataModel;

namespace DBData.DBModel
{
    public class DBPropertyMetadata : BasePropertyMetadata
    {
        #region Constructor

        public DBPropertyMetadata()
        {
            TypeProperties = new HashSet<DBTypeMetadata>();
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

        public virtual ICollection<DBTypeMetadata> TypeProperties { get; set; }

        #endregion

    }
}