using System.ComponentModel.DataAnnotations.Schema;

namespace ApiPetShop.Domain
{
    public class ModelBase
    {
        [Column(Order = 1)]
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate {  get; set; } = DateTime.Now;


        public void DeleteFlag()
        {
            IsDeleted = true;
            UpdateModel();
        }

        public void UpdateModel()
        {
            UpdatedDate = DateTime.Now;
        }
    }
}
