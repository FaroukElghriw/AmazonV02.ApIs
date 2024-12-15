using AmazonV02.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AmazonV02.Repository.Data
{
	 public static class AmazonDbcontextDataSeed
	{
		public static async Task DataSeedingasync(AmazonDbContext amazonDb)
		{
			
			if (!amazonDb.Brnands.Any())
			{
				var brandData = File.ReadAllText("C:\\Users\\Dell\\source\\repos\\AmazonV02.ApIs\\AmazonV02.Repository\\Data\\DataSeed\\brands.json");
				var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);
				if (brands is not  null && brands.Count > 0)
				{
					foreach (var brand in brands)
					           await amazonDb.Set<ProductBrand>().AddAsync(brand);

						await amazonDb.SaveChangesAsync();
					
				}

			}
			
			if (!amazonDb.Types.Any())
			{
				var typeData = File.ReadAllText("C:\\Users\\Dell\\source\\repos\\AmazonV02.ApIs\\AmazonV02.Repository\\Data\\DataSeed\\types.json");
				var types = JsonSerializer.Deserialize<List<ProductType>>(typeData);
				if (types is not null && types.Count > 0)
				{
					foreach (var type in types)
					await amazonDb.Set<ProductType>().AddAsync(type);

						await amazonDb.SaveChangesAsync();
					
				}

			}
		
			if (!amazonDb.Products.Any())
			{
				var productData = File.ReadAllText("C:\\Users\\Dell\\source\\repos\\AmazonV02.ApIs\\AmazonV02.Repository\\Data\\DataSeed\\products.json");
				var products = JsonSerializer.Deserialize<List<Product>>(productData);
				if (products is not null && products.Count > 0)
				{
					foreach (var product in products)
				               await amazonDb.Set<Product>().AddAsync(product);


						await amazonDb.SaveChangesAsync();
					
				}

			}
		}
	}
}
