using APIstarts_API.Models.Dto;

namespace APIstarts_API.Data
{
    public static class VillaStore   //API'de kullanılacak olan örnek verileri tutar.
    {
        public static List<VillaDTO> villaList = new List<VillaDTO>
            {
                new VillaDTO{Id=1, Name="Pool view", Sqft=100, Occupancy = 300},
                new VillaDTO{Id=2, Name="Beach view",Sqft= 200, Occupancy= 400 }
            };
    }
}
