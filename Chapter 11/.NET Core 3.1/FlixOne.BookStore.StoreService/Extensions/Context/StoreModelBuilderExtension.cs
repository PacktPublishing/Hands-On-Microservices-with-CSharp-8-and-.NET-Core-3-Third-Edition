using FlixOne.BookStore.StoreService.Models;
using FlixOne.BookStore.StoreService.Models.Relations;
using Microsoft.EntityFrameworkCore;
using System;
namespace FlixOne.BookStore.StoreService.Extensions.Context
{
    /// <summary>
    /// Seed data
    /// </summary>
    public static class StoreModelBuilderExtension
    {
        /// <summary>
        /// Seed data
        /// </summary>
        /// <param name="modelBuilder">model builder</param>
        public static void Seed(this ModelBuilder modelBuilder)
        {
            SeedStoreInfo(modelBuilder);
            SeedStoreAddress(modelBuilder);
            SeedBookType(modelBuilder);
            SeedCategory(modelBuilder);
            SeedProduct(modelBuilder);
            SeedImage(modelBuilder);
            SeedProductAttribute(modelBuilder);
            SeedProductAttributeRelationship(modelBuilder);
            SeedProductImageRelationship(modelBuilder);
        }

        private static void SeedStoreInfo(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StoreInfo>().HasData(
                            new StoreInfo
                            {
                                Id = new Guid("d332328a-1ed2-4886-85fc-d5e80af1e207"),
                                //ParentStoreId = "d332328a-1ed2-4886-85fc-d5e80af1e207",
                                Name = "FlixOne Inc.",
                                Description = "FlixOne Store powered by PACKT Publishing",
                                URL = "https://flixone.com",
                                Email = "info@flixone.com",
                                PhoneNumber = "1234567890"
                            }
                        );
        }

        private static void SeedStoreAddress(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StoreAddress>().HasData(
                new StoreAddress
                {
                    Id = new Guid("8e68a87e-6023-4d92-7b72-08d7d48a1c4f"),
                    StoreId = new Guid("d332328a-1ed2-4886-85fc-d5e80af1e207"),
                    AddressLine1 = "3710 Westwinds Dr NE #24",
                    City = "Calgary",
                    State = "Alberta",
                    Zip = "AB T3J 5H3",
                    Country = "Canada"
                }
            );
        }

        private static void SeedBookType(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookType>().HasData(
                            new BookType
                            {
                                Id = new Guid("d2492b3b-8f98-4779-9fab-5fcc07acde1c"),
                                Name = "Technical",
                                Description = "All technical books can also contain accademic books"
                            },
                            new BookType
                            {
                                Id = new Guid("e1f8f439-f63b-4265-bba6-b59f975b5f20"),
                                Name = "Non-Technical",
                                Description = "All book which are not covered under Technical book type"
                            });
        }

        private static void SeedCategory(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                            new Category
                            {
                                Id = new Guid("10233074-12eb-4478-8f97-f83733cc171a"),
                                BookTypeId = new Guid("d2492b3b-8f98-4779-9fab-5fcc07acde1c"),
                                Name = "Java",
                                Description = "All Java Books"

                            },
                            new Category
                            {
                                Id = new Guid("d127aafd-4901-43a8-b8c6-9e197f429438"),
                                BookTypeId = new Guid("d2492b3b-8f98-4779-9fab-5fcc07acde1c"),
                                Name = "Python",
                                Description = "All Python Books"

                            },
                            new Category
                            {
                                Id = new Guid("d7c8a82f-0bd8-4680-9ff4-69d976452c5d"),
                                BookTypeId = new Guid("d2492b3b-8f98-4779-9fab-5fcc07acde1c"),
                                Name = "Python",
                                Description = "All Python Books"

                            },
                            new Category
                            {
                                Id = new Guid("e6008e1e-1233-4341-9407-996e3532b184"),
                                BookTypeId = new Guid("d2492b3b-8f98-4779-9fab-5fcc07acde1c"),
                                Name = "C#",
                                Description = "All CSharp Books"

                            },
                            new Category
                            {
                                Id = new Guid("86f23225-7032-44dc-ab7a-c9b474c9ba89"),
                                BookTypeId = new Guid("e1f8f439-f63b-4265-bba6-b59f975b5f20"),
                                Name = "Fantasy",
                                Description = "All Fantasy Books"

                            },
                            new Category
                            {
                                Id = new Guid("3f5eb0c8-f1b7-4c1e-9d7e-f02dd2479670"),
                                BookTypeId = new Guid("e1f8f439-f63b-4265-bba6-b59f975b5f20"),
                                Name = "Fantasy",
                                Description = "All Fantasy Books"

                            },
                            new Category
                            {
                                Id = new Guid("857e65a6-c9df-4d9c-996e-a5f0ab0b1298"),
                                BookTypeId = new Guid("e1f8f439-f63b-4265-bba6-b59f975b5f20"),
                                Name = "Science Fiction",
                                Description = "All SciFi Books"

                            },
                            new Category
                            {
                                Id = new Guid("d3265880-09ba-475b-a69a-8e60ea2240b7"),
                                BookTypeId = new Guid("e1f8f439-f63b-4265-bba6-b59f975b5f20"),
                                Name = "Historical Fiction",
                                Description = "All Historical Books"

                            },
                            new Category
                            {
                                Id = new Guid("4bb70eca-77f0-44e2-b0db-01ab779a4893"),
                                BookTypeId = new Guid("e1f8f439-f63b-4265-bba6-b59f975b5f20"),
                                Name = "Realistic Fiction",
                                Description = "All Realistic Fiction Books"

                            },
                            new Category
                            {
                                Id = new Guid("9bc67535-76fd-47a4-ae6d-3dab6fc2e2f5"),
                                BookTypeId = new Guid("e1f8f439-f63b-4265-bba6-b59f975b5f20"),
                                Name = "Fan Fiction",
                                Description = "All Fan Fiction Books"

                            },
                            new Category
                            {
                                Id = new Guid("265f027d-716d-4cc8-bf8b-ad529dd3816a"),
                                BookTypeId = new Guid("e1f8f439-f63b-4265-bba6-b59f975b5f20"),
                                Name = "Crime",
                                Description = "All Crime Books"

                            },
                            new Category
                            {
                                Id = new Guid("814cb12c-864e-4887-8a36-f9e8977aaad1"),
                                BookTypeId = new Guid("e1f8f439-f63b-4265-bba6-b59f975b5f20"),
                                Name = "Mystery",
                                Description = "All Mystery Books"

                            },
                            new Category
                            {
                                Id = new Guid("3207d5e3-f6cc-4168-92a8-41ce568fb235"),
                                BookTypeId = new Guid("e1f8f439-f63b-4265-bba6-b59f975b5f20"),
                                Name = "Suspense",
                                Description = "All Suspense/Thrillers Books"

                            },
                            new Category
                            {
                                Id = new Guid("4d11672a-3d6d-4ab2-86d4-dd89964bb8a7"),
                                BookTypeId = new Guid("e1f8f439-f63b-4265-bba6-b59f975b5f20"),
                                Name = "Horror",
                                Description = "All Horror Books"

                            },
                            new Category
                            {
                                Id = new Guid("21a526ce-ed92-47f4-9a2d-7ffc1ecace8f"),
                                BookTypeId = new Guid("e1f8f439-f63b-4265-bba6-b59f975b5f20"),
                                Name = "Humor",
                                Description = "All Humor Books"

                            },
                            new Category
                            {
                                Id = new Guid("8b181264-8eb9-4238-b203-e06c9605d06e"),
                                BookTypeId = new Guid("e1f8f439-f63b-4265-bba6-b59f975b5f20"),
                                Name = "Classic",
                                Description = "All Classic Books"

                            },
                            new Category
                            {
                                Id = new Guid("e6762919-767d-41cf-8421-dd5bb6233f73"),
                                BookTypeId = new Guid("e1f8f439-f63b-4265-bba6-b59f975b5f20"),
                                Name = "Satire",
                                Description = "All Satire Books"

                            },
                            new Category
                            {
                                Id = new Guid("75f0db8e-d7d9-4979-8726-fd1d9014db5f"),
                                BookTypeId = new Guid("e1f8f439-f63b-4265-bba6-b59f975b5f20"),
                                Name = "Romance",
                                Description = "All Romance Books"

                            },
                            new Category
                            {
                                Id = new Guid("c77e17c6-9018-41ab-b8c2-43c5d1561b06"),
                                BookTypeId = new Guid("e1f8f439-f63b-4265-bba6-b59f975b5f20"),
                                Name = "Drama",
                                Description = "All Drama Books"

                            },
                            new Category
                            {
                                Id = new Guid("87aca1d7-0e2f-40a3-a4dd-2c4729737ecc"),
                                BookTypeId = new Guid("e1f8f439-f63b-4265-bba6-b59f975b5f20"),
                                Name = "Anthology",
                                Description = "All Anthology Books"

                            },
                            new Category
                            {
                                Id = new Guid("b6441361-1604-42e0-bd98-9dbd6bc8b8e6"),
                                BookTypeId = new Guid("e1f8f439-f63b-4265-bba6-b59f975b5f20"),
                                Name = "Fable",
                                Description = "All Fable Books"

                            },
                            new Category
                            {
                                Id = new Guid("f68b7972-4640-4d11-80e0-fab1e9167bf5"),
                                BookTypeId = new Guid("e1f8f439-f63b-4265-bba6-b59f975b5f20"),
                                Name = "Fairy Tale",
                                Description = "All Fairy Tale Books"

                            },
                            new Category
                            {
                                Id = new Guid("214bd186-460d-4996-a0ba-4bcf2455583e"),
                                BookTypeId = new Guid("e1f8f439-f63b-4265-bba6-b59f975b5f20"),
                                Name = "Short Story",
                                Description = "All Short Story Books"

                            },
                            new Category
                            {
                                Id = new Guid("c1e5d3d1-e090-4106-ae77-ceaa33df6b6d"),
                                BookTypeId = new Guid("e1f8f439-f63b-4265-bba6-b59f975b5f20"),
                                Name = "Legend",
                                Description = "All Legend Books"

                            },
                            new Category
                            {
                                Id = new Guid("432a628b-cdb0-45bf-a00a-2dfb49f9a4d6"),
                                BookTypeId = new Guid("e1f8f439-f63b-4265-bba6-b59f975b5f20"),
                                Name = "Mythology",
                                Description = "All Mythology Books"

                            },
                            new Category
                            {
                                Id = new Guid("41bd7c8d-91cf-4955-b2e5-2249b6392a57"),
                                BookTypeId = new Guid("e1f8f439-f63b-4265-bba6-b59f975b5f20"),
                                Name = "Biography",
                                Description = "All Biography Books"

                            },
                            new Category
                            {
                                Id = new Guid("832dfde1-c2d0-42d2-b8eb-ef0ae9c821b8"),
                                BookTypeId = new Guid("e1f8f439-f63b-4265-bba6-b59f975b5f20"),
                                Name = "Periodicals",
                                Description = "All Periodicals Books"

                            },
                            new Category
                            {
                                Id = new Guid("9921d556-df88-440f-8743-05fc76ead060"),
                                BookTypeId = new Guid("e1f8f439-f63b-4265-bba6-b59f975b5f20"),
                                Name = "Self-Help",
                                Description = "All Self-Help Books"

                            },
                            new Category
                            {
                                Id = new Guid("64ddcb2b-9633-467f-976b-ee832b487e9b"),
                                BookTypeId = new Guid("e1f8f439-f63b-4265-bba6-b59f975b5f20"),
                                Name = "Reference",
                                Description = "All Reference Books"

                            },
                            new Category
                            {
                                Id = new Guid("7397bbb2-e11b-4252-bfbe-8657fd35b872"),
                                BookTypeId = new Guid("e1f8f439-f63b-4265-bba6-b59f975b5f20"),
                                Name = "Speech",
                                Description = "All Speech Books"

                            },
                            new Category
                            {
                                Id = new Guid("ee243253-3c0f-40b8-a8e8-562f0b81b481"),
                                BookTypeId = new Guid("e1f8f439-f63b-4265-bba6-b59f975b5f20"),
                                Name = "Essay",
                                Description = "All Essay Books"

                            });
        }

        private static void SeedProduct(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                            new Product
                            {
                                Id = new Guid("0aff379a-3e94-4547-8564-7eda7cb5d3b5"),
                                CategoryId = new Guid("e6008e1e-1233-4341-9407-996e3532b184"),
                                Name = "Hands-On Design Patterns with C# and .NET Core",
                                ShortName = "Design Patterns",
                                Description = "Build effective applications in C# and .NET Core by using proven programming practices and design techniques.",
                                Available = true,
                                StoreId = new Guid("d332328a-1ed2-4886-85fc-d5e80af1e207"),
                                MinReorderLevel = 2,
                                QtyInStock = 10,
                                UnitPrice = 365M
                            },
                            new Product
                            {
                                Id = new Guid("9a377c89-58e4-4b9f-8ef0-6ef92f902ecb"),
                                CategoryId = new Guid("e6008e1e-1233-4341-9407-996e3532b184"),
                                Name = "C# 7 and .NET: Designing Modern Cross-platform Applications",
                                ShortName = "C# 7 and .NET",
                                Description = "Explore C# and the .NET Core framework to create applications and optimize them with ASP.NET Core 2",
                                Available = true,
                                StoreId = new Guid("d332328a-1ed2-4886-85fc-d5e80af1e207"),
                                MinReorderLevel = 2,
                                QtyInStock = 10,
                                UnitPrice = 365M
                            }
                            );
        }

        private static void SeedImage(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Image>().HasData(
                            new Image
                            {
                                Id = new Guid("58f370cf-92ec-4310-8811-8ebf5283de5e"),
                                Name = "CSharp",
                                Path = "/images/csharp.jpg",
                                Url = "https://flixone.com/images/csharp.jpg"
                            }
                            );
        }

        private static void SeedProductAttribute(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductAttribute>().HasData(
                new ProductAttribute
                {
                    Id = new Guid("caee136b-8d2a-486e-b132-e7499cc83a57"),
                    Name = "Best Seller",
                    Icon = "bestseller.jpg"

                }
                );
        }

        private static void SeedProductAttributeRelationship(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductAttributeRelationship>().HasData(new ProductAttributeRelationship
            {
                Id = new Guid("5bcb0a0f-4f1e-4995-933e-56629240092e"),
                AttributeId = new Guid("caee136b-8d2a-486e-b132-e7499cc83a57"),
                ProductId = new Guid("0aff379a-3e94-4547-8564-7eda7cb5d3b5")
            });
        }

        private static void SeedProductImageRelationship(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductImageRelationship>().HasData(new ProductImageRelationship
            {
                Id = new Guid("b49cb7cd-c5c3-4ab7-9000-3d70ff6dc7f8"),
                ImageId = new Guid("58f370cf-92ec-4310-8811-8ebf5283de5e"),

            });
        }
    }
}
