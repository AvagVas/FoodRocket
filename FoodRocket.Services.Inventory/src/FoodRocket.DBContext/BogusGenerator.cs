using Bogus;
using Bogus.Extensions.UnitedStates;
using FoodRocket.DBContext.Models;
using FoodRocket.DBContext.Models.Customers;
using FoodRocket.DBContext.Models.Inventory;
using FoodRocket.DBContext.Models.Orders;
using FoodRocket.DBContext.Models.Staff;
using FoodRocket.DBContext.Services;

namespace FoodRocket.DBContext;

public class BogusGenerator
{
    public class DishEqualityComparer : IEqualityComparer<DishMenu>
    {
        public bool Equals(DishMenu x, DishMenu y)
        {
            return x.DishId == y.DishId;
        }

        public int GetHashCode (DishMenu obj)
        {
            return obj.DishId.GetHashCode();
        }
    }
    private IEnumerable<string> _dishes = new[]

        #region dishes

        {
            "Consomme printaniere royal",
            "Chicken gumbo",
            "Tomato aux croutons",
            "Onion au gratin",
            "St. Emilion",
            "Radishes",
            "Chicken soup with rice",
            "Clam broth (cup)",
            "Cream of new asparagus, croutons",
            "Clear green turtle",
            "Striped bass saute, meuniere",
            "Anchovies",
            "Fresh lobsters in every style",
            "Celery",
            "Pim-olas",
            "Caviar",
            "Sardines",
            "India chutney",
            "Pickles",
            "English walnuts",
            "Pate de foies-gras",
            "Pomard",
            "Brook trout, mountain style",
            "Whitebait, sauce tartare",
            "Clams",
            "Oysters",
            "Claremont planked shad",
            "G. H. Mumm & Co's Extra Dry",
            "Cerealine with Milk",
            "Sliced Bananas",
            "Wheat Vitos",
            "Sliced Tomatoes",
            "Russian Caviare on Toast",
            "Smoked beef in cream",
            "Apple Sauce",
            "Potage a la Victoria",
            "Breakfast",
            "Strawberries",
            "Preserved figs",
            "BLUE POINTS",
            "CONSOMME ANGLAISE",
            "CREAM OF CAULIFLOWER",
            "BROILED SHAD, A LA MAITRE D'HOTEL",
            "SLICED CUCUMBERS",
            "SALTED ALMONDS",
            "POTATOES, JULIEN",
            "Cracked Wheat",
            "Malt Breakfast Food",
            "BOILED BEEF TONGUE, ITALIAN SAUCE",
            "Young Onions",
            "Pears",
            "ROAST SIRLOIN OF BEEF, YORKSHIRE PUDDING",
            "Huhnerbruhe",
            "ROAST EASTER LAMB, MINT SAUCE",
            "Rockaways",
            "Hafergrutze",
            "BROWNED POTATOES",
            "Pampelmuse",
            "Apfelsinen",
            "Ananas",
            "Milchreis",
            "Grape fruit",
            "Oranges",
            "Clam Fritters",
            "Filet v. Schildkrote m. Truffeln",
            "Bouillon, en Tasse",
            "Spargel Suppe",
            "Kraftsuppe, konigliche Art",
            "Rissoles a la Merrill",
            "S. Julien",
            "Chambertin",
            "St. Julien",
            "Vegetable",
            "Puree of split peas aux croutons",
            "Consomme in cup",
            "Broiled shad, Maitre d'hotel",
            "Mashed potatoes",
            "Breaded veal cutlet with peas",
            "Hind-quarter of spring lamb with stuffed tomatoes",
            "Hot or cold ribs of beef",
            "Doucette salad",
            "New beets",
            "Salisbury steak au cresson",
            "Boiled rice",
            "Stewed oyster plant",
            "Boiled onions, cream sauce",
            "Old fashioned rice pudding",
            "Ice cream",
            "Coffee",
            "Tea",
            "Milk",
            "Mush",
            "Rolled Oats",
            "Small Hominy",
            "Broiled Mackerel",
            "Kippered Herring",
            "Strawberries with cream",
            "Compote of fruits",
            "Orange marmalade",
            "Baked apples with cream",
            "Bananas",
            "Bananas with cream",
            "Austern in der Schale",
            "Stewed prunes",
            "Fruit",
            "Grapes",
            "Honey in comb",
            "Apples",
            "Oranges sliced",
            "Stewed tomatoes",
            "Large pot of coffee",
            "Cup of coffee (served in small pot)",
            "Large pot of Oolong tea",
            "Pot of chocolate",
            "Pitcher of milk",
            "Pot of broma",
            "Suppe, Schlossherrin Art",
            "Eggs, boiled [2]",
            "Sliced Bananas and Cream",
            "Baked Apples and Cream",
            "Pettijohn Breakfast Food",
            "Quaker Oats",
            "Hotch potch von Ochsenschwanazen",
            "Sugar Cured Ham",
            "PINE APPLE FRITTERS, SAUCE MARASCHINO",
            "Planked shad",
            "Baked bluefish, Duxelle",
            "Consomme aux Quenelle's",
            "Milk rice",
            "Mohren Suppe mit Sago",
            "French rolls",
            "Milk rolls",
            "Corn muffins",
            "Omelette aux fines herbes",
            "Boiled eggs",
            "Bacon and eggs",
            "Fish cakes",
            "Food",
            "Shreaded Wheat",
            "Clam cocktail",
            "Oyster cocktail",
            "Little Neck clams",
            "Stuffed olives",
            "Lalla Rookh",
            "Malaga grapes",
            "Martineaus",
            "Oatmeal",
            "hominy with milk",
            "Baked apples",
            "PATTIES OF SWEET BREADS, A LA TOULOUSE",
            "Chicken broth, per cup",
            "Terrapin, Maryland",
            "Sardines on toast",
            "English snipe",
            "Charlotte Russe",
            "Tutti frutti",
            "Lady fingers",
            "Marrow bones on toast",
            "Fresh mushrooms on toast",
            "Farm Sausage",
            "Club sandwich",
            "Baked Stuffed Mullet & Sauce Pomard",
            "Thon marine",
            "Croquettes of sweetbreads",
            "Teal duck",
            "Demi-tasse",
            "Strawberry",
            "Oyster Bay asparagus",
            "SMALL TENDERLOIN STEAK, A LA STANLEY",
            "Hoot Mon Mush",
            "Apollinoris",
            "Vve Cliquot",
            "Chicken broth",
            "Fruits in Season",
            "Salt Codfish, Spanish Style",
            "Oatmeal Porridge",
            "Mashed Hominy",
            "Plain Omelette",
            "Canape, Martha",
            "Puree of beans",
            "Fried Flounders",
            "Strawberry short cake",
            "Fried smelts",
            "Fried fish",
            "Oxford Sausage",
            "BUZZARD BAY OYSTERS",
            "Strained gumbo",
            "Bouillon in cup",
            "Queen olives",
            "Chow chow",
            "Panfish, Meuniere",
            "German fried potatoes",
            "Ribs of prime beef",
            "Calf's tongue, caper sauce",
            "Luncheon",
            "Assorted cakes",
            "Scollops en caisse, Supreme",
            "Irish stew",
            "Marrow on toast, Bordelaise"
        };

    #endregion dishes

    private readonly INewIdGenerator _idGenerator;

    public BogusGenerator(INewIdGenerator idGenerator)
    {
        _idGenerator = idGenerator;
    }

    public Data GetData()
    {
        //Randomizer.Seed = new Random(8675309);

        UnitOfMeasure kg, item, litr, gram, box100;
        IEnumerable<UnitOfMeasure> weights = new[]
        {
            kg = new UnitOfMeasure()
            {
                UnitOfMeasureId = _idGenerator.GetNewIdFor("uom"), Name = "Kg", IsBase = true, Ratio = 1,
                IsFractional = false, BaseOfUnitOfMId = null, Type = TypeOfUnitOfMeasure.Weight
            },

            gram = new UnitOfMeasure()
            {
                UnitOfMeasureId = _idGenerator.GetNewIdFor("uom"), Name = "Gram", IsBase = false, Ratio = 1000,
                IsFractional = true, BaseOfUnitOfMId = kg.UnitOfMeasureId, Type = TypeOfUnitOfMeasure.Weight
            }
        };

        IEnumerable<UnitOfMeasure> pieces = new[]
        {
            item = new UnitOfMeasure()
            {
                UnitOfMeasureId = _idGenerator.GetNewIdFor("uom"), Name = "Item", IsBase = true, Ratio = 1,
                IsFractional = false, BaseOfUnitOfMId = null, Type = TypeOfUnitOfMeasure.Piece
            },

            box100 = new UnitOfMeasure()
            {
                UnitOfMeasureId = _idGenerator.GetNewIdFor("uom"), Name = "Box100", IsBase = false, Ratio = 100,
                IsFractional = false, BaseOfUnitOfMId = item.UnitOfMeasureId, Type = TypeOfUnitOfMeasure.Piece
            }
        };

        IEnumerable<UnitOfMeasure> volumes = new[]
        {
            litr = new UnitOfMeasure()
            {
                UnitOfMeasureId = _idGenerator.GetNewIdFor("uom"), Name = "Litre", IsBase = true, Ratio = 1,
                IsFractional = false, BaseOfUnitOfMId = null, Type = TypeOfUnitOfMeasure.Volume
            },
        };

        var typesOfUoM = new[]
        {
            "weights",
            "pieces",
            "volumes"
        };

        TypeOfUnitOfMeasure selectedType = TypeOfUnitOfMeasure.Weight;
        Dictionary<string, int> productsIndexes = new Dictionary<string, int>();
        var productFaker = new Faker<Product>()
            .RuleFor(product => product.ProductId, f => _idGenerator.GetNewIdFor("product"))
            .RuleFor(product => product.Name, f =>
            {
                var productName = f.Commerce.Product();
                string index = "";
                if (productsIndexes.ContainsKey(productName))
                {
                    productsIndexes[productName]++;
                    index = $"_{productsIndexes[productName].ToString()}";
                }
                else
                {
                    productsIndexes[productName] = 0;
                }


                return $"{productName}{index}";
            })
            .RuleFor(product => product.MainUnitOfMeasure, f =>
            {
                selectedType = f.PickRandom<TypeOfUnitOfMeasure>();
                if (selectedType == TypeOfUnitOfMeasure.Weight)
                {
                    return f.PickRandom(weights);
                }

                if (selectedType == TypeOfUnitOfMeasure.Piece)
                {
                    return f.PickRandom(pieces);
                }

                return f.PickRandom(volumes);
            })
            .RuleFor(product => product.UnitOfMeasures, f =>
            {
                if (selectedType == TypeOfUnitOfMeasure.Weight)
                {
                    return weights.AsEnumerable();
                }

                if (selectedType == TypeOfUnitOfMeasure.Piece)
                {
                    return pieces.AsEnumerable();
                }

                return volumes.AsEnumerable();
            });

        var products = productFaker.Generate(100);
        Storage mainStorage, reserveStorage;
        IEnumerable<Storage> storages = new[]
        {
            mainStorage = new Storage()
            {
                StorageId = _idGenerator.GetNewIdFor("storage"),
                ManagerId = null,
                Name = "Main Storage",
            },
            reserveStorage = new Storage()
            {
                StorageId = _idGenerator.GetNewIdFor("storage"),
                ManagerId = null,
                Name = "Reserve",
            }
        };

        List<StorageProduct> storageProducts = new List<StorageProduct>();
        Random r = new Random();
        foreach (var product in products)
        {
            var mainStorageProduct = new StorageProduct();
            mainStorageProduct.ProductId = product.ProductId;
            mainStorageProduct.StorageId = mainStorage.StorageId;
            mainStorageProduct.Product = product;
            mainStorageProduct.Storage = mainStorage;
            mainStorageProduct.UnitOfMeasure = product.MainUnitOfMeasure;
            mainStorageProduct.Quantity = r.Next(0, 100);
            var reserveStorageProduct = new StorageProduct();
            reserveStorageProduct.ProductId = product.ProductId;
            reserveStorageProduct.StorageId = reserveStorage.StorageId;
            reserveStorageProduct.Product = product;
            reserveStorageProduct.Storage = reserveStorage;
            reserveStorageProduct.UnitOfMeasure = product.MainUnitOfMeasure;
            reserveStorageProduct.Quantity = r.Next(0, 100);

            storageProducts.Add(mainStorageProduct);
            storageProducts.Add(reserveStorageProduct);
        }


        Dictionary<string, int> dishUniqueIndexex = new Dictionary<string, int>();
        var dishFaker = new Faker<Dish>()
            .RuleFor(dish => dish.DishId, (f, dish) => _idGenerator.GetNewIdFor("dish"))
            .RuleFor(dish => dish.Name, f =>
            {
                var dishName = f.PickRandom(_dishes);
                string index = "";
                if (dishUniqueIndexex.ContainsKey(dishName))
                {
                    dishUniqueIndexex[dishName]++;
                    index = $"_{dishUniqueIndexex[dishName].ToString()}";
                }
                else
                {
                    dishUniqueIndexex[dishName] = 0;
                }


                return $"{dishName}{index}";
            })
            .RuleFor(dish => dish.Description, f => f.Lorem.Sentence());

        var fakeDishes = dishFaker.Generate(200);
        Menu mainMenu = new Menu();

        List<Menu> menus = new() { mainMenu };
        mainMenu.MenuId = _idGenerator.GetNewIdFor("menu");
        mainMenu.Version = 1;
        mainMenu.ChangedBy = _idGenerator.GetNewIdFor("user");
        mainMenu.CreatedOn = DateTime.Today;
        mainMenu.Name = "Our Main Menu";

        List<Ingredient> ingredients = new();
        foreach (var product in products)
        {
            var ingredient = new Ingredient();
            ingredient.IngredientId = _idGenerator.GetNewIdFor("ingredient");
            ingredient.ProductId = product.ProductId;
            ingredient.ProductName = product.Name;

            ingredients.Add(ingredient);
        }

        List<IngredientDish> dishesWithIngredients = new();
        foreach (var dish in fakeDishes)
        {
            int amountToPick = r.Next(1, 8);
            var someIngredients = new Faker().PickRandom(ingredients, amountToPick);
            foreach (var selectedIngredient in someIngredients)
            {
                IngredientDish ingredientDish = new();
                ingredientDish.DishId = dish.DishId;
                ingredientDish.IngredientId = selectedIngredient.IngredientId;
                ingredientDish.RequiredQuantity = r.Next(4, 12);

                var productIngredient = products.Find(product => product.ProductId == selectedIngredient.ProductId);
                ingredientDish.RequiredInUnitOfMeasureId = productIngredient!.MainUnitOfMeasure.UnitOfMeasureId;
                ingredientDish.NameOfUnitOfMeasureId = productIngredient.MainUnitOfMeasure.Name;
                dishesWithIngredients.Add(ingredientDish);
            }
        }

        List<DishMenu> dishesInMenu = new();
        int order = 1;
        foreach (var dish in fakeDishes)
        {
            var dishMenu = new DishMenu();
            dishMenu.DishId = dish.DishId;
            dishMenu.MenuId = mainMenu.MenuId;
            dishMenu.Version = mainMenu.Version;
            dishMenu.Order = order++;
            dishMenu.Price = decimal.Parse(new Faker().Commerce.Price());
            dishMenu.PublishedOn = DateTime.Today;
            dishesInMenu.Add(dishMenu);
        }

        int promotionsCount = r.Next(0, dishesInMenu.Count);
        List<long> selectedDishIds = new();
        List<PriceOffer> promotions = new();

        var faker = new Faker();
        for (int promotionIndex = 0; promotionIndex < promotionsCount; promotionIndex++)
        {
            DishMenu selectedDish = null;
            do
            {
                selectedDish = new Faker().PickRandom(dishesInMenu);
            } while (selectedDishIds.Contains(selectedDish.DishId));

            selectedDishIds.Add(selectedDish.DishId);
            var priceOffer = new PriceOffer();
            priceOffer.DishId = selectedDish.DishId;
            priceOffer.PromotionalText = $"SUPER PRICE: {faker.Lorem.Sentence(4)}";
            priceOffer.NewPrice = selectedDish.Price * (decimal)0.75;
            selectedDish.Promotion = priceOffer;
            promotions.Add(priceOffer);
        }


        var customersFakers = new Faker<Customer>()
            .RuleFor(customer => customer.CustomerId, f => _idGenerator.GetNewIdFor("customer"))
            .RuleFor(customer => customer.FirstName, f => f.Person.FirstName)
            .RuleFor(customer => customer.LastName, f => f.Person.LastName)
            .RuleFor(customer => customer.IsActive, f => true)
            .RuleFor(customer => customer.IsDeleted, f => false)
            .RuleFor(customer => customer.CreatedAt, f => DateTime.Today);


        var customers = customersFakers.Generate(200);


        List<Contact> contacts = new();
        foreach (var customer in customers)
        {
            int contactsCount = r.Next(2, 6);
            for (int ci = 0; ci < contactsCount; ci++)
            {
                ContactType selectedCobtactType =
                    faker.PickRandom(Enum.GetValues(typeof(ContactType)).Cast<ContactType>());
                Contact contact = new();
                contact.ContactId = _idGenerator.GetNewIdFor("contact");
                contact.Name = $"{customer.FirstName}'s {selectedCobtactType}";
                if (selectedCobtactType == ContactType.Email)
                {
                    contact.Value = faker.Internet.Email(customer.FirstName, customer.LastName, "email.com", "x");
                }
                else if (selectedCobtactType == ContactType.Phone)
                {
                    contact.Value = faker.Phone.PhoneNumber("(###) ###-####");
                }
                else if (selectedCobtactType == ContactType.Mobile)
                {
                    contact.Value = faker.Phone.PhoneNumber("(###) ###-####");
                }
                else if (selectedCobtactType == ContactType.Telegram)
                {
                    contact.Value = $"@{customer.FirstName}_{customer.LastName}";
                }


                contact.ContactType = selectedCobtactType;
                contact.IsPrimary = ci == 0;
                if (contact.IsPrimary)
                {
                    customer.PrimaryContactId = contact.ContactId;
                }
                contact.Customer = customer;
                contacts.Add(contact);
            }
        }

        List<Address> addresses = new();
        foreach (var customer in customers)
        {
            int addressePerCustomerCount = r.Next(2, 5);
            for (int ai = 0; ai < addressePerCustomerCount; ai++)
            {
                var address = new Address();
                address.AddressId = _idGenerator.GetNewIdFor("address");
                address.Country = faker.Address.Country();
                address.State = faker.Address.State();
                address.City = faker.Address.City();
                address.AddressLine = faker.Address.FullAddress();
                address.ZipCode = faker.Address.ZipCode();
                address.IsActive = true;
                address.IsDeleted = true;
                address.Customer = customer;
                if (ai == 0)
                {
                    customer.MainBillingAddressId = address.AddressId;
                }

                if (ai == addressePerCustomerCount - 1)
                {
                    customer.MainShippingAddressId = address.AddressId;
                }

                addresses.Add(address);
            }
        }

        var employeesFaker = new Faker<Employee>()
            .RuleFor(employee => employee.EmployeeId, f => _idGenerator.GetNewIdFor("employee"))
            .RuleFor(employee => employee.FirstName, f => f.Person.FirstName)
            .RuleFor(employee => employee.LastName, f => f.Person.LastName)
            .RuleFor(employee => employee.SocialSecurityNumber, f => f.Person.Ssn())
            .RuleFor(employee => employee.Address, f => f.Address.FullAddress())
            .RuleFor(employee => employee.Phone, f => f.Person.Phone)
            .RuleFor(employee => employee.IsDeleted, f => false);

        var employees = employeesFaker.Generate(50);

        List<long> selectedEmployeesId = new();
        var managersFaker = new Faker<Manager>()
            .RuleFor(manager => manager.ManagerId, f => _idGenerator.GetNewIdFor("manager"))
            .RuleFor(manager => manager.StartedOn, f => f.Date.Past())
            .RuleFor(manager => manager.Employee, f =>
            {
                Employee selectedEmployee;
                do
                {
                    selectedEmployee = f.PickRandom(employees);
                } while (selectedEmployeesId.Contains(selectedEmployee.EmployeeId));

                selectedEmployeesId.Add(selectedEmployee.EmployeeId);

                return selectedEmployee;
            });

        var managers = managersFaker.Generate(5);

        selectedEmployeesId.Clear();
        var waitersFaker = new Faker<Waiter>()
            .RuleFor(waiter => waiter.WaiterId, f => _idGenerator.GetNewIdFor("waiter"))
            .RuleFor(waiter => waiter.StartedOn, f => f.Date.Past())
            .RuleFor(waiter => waiter.Employee, f =>
            {
                Employee selectedEmployee;
                do
                {
                    selectedEmployee = f.PickRandom(employees);
                } while (selectedEmployeesId.Contains(selectedEmployee.EmployeeId));

                selectedEmployeesId.Add(selectedEmployee.EmployeeId);

                return selectedEmployee;
            })
            .RuleFor(waiter => waiter.Manager, f => f.PickRandom(managers));

        var waiters = waitersFaker.Generate(10);

        var ordersFaker = new Faker<Order>()
            .RuleFor(order => order.OrderId, f => _idGenerator.GetNewIdFor("order"))
            .RuleFor(order => order.CreateOn, f => f.Date.Past())
            .RuleFor(order => order.Status, f => f.PickRandom(Enum.GetValues(typeof(OrderStatus)).Cast<OrderStatus>()))
            .RuleFor(order => order.CustomerId, f => f.PickRandom(customers).CustomerId)
            .RuleFor(order => order.ManagerId, f => f.PickRandom(managers).ManagerId)
            .RuleFor(order => order.WaiterId, f => f.PickRandom(waiters).WaiterId)
            .RuleFor(order => order.Items, (f, order) =>
            {
                List<OrderItem> orderItems = new();
                var orderedDishesFromMenu = f.PickRandom(dishesInMenu, r.Next(2, 14));
                var uniqueDishes = orderedDishesFromMenu.Distinct(new DishEqualityComparer());
                decimal totalSum = 0;
                foreach (var dish in uniqueDishes)
                {
                    var orderItem = new OrderItem();
                    orderItem.OrderItemId = _idGenerator.GetNewIdFor("order_item");
                    orderItem.Order = order;
                    orderItem.DishFromMenu = dish;
                    orderItem.Quantity = r.Next(1, 6);
                    orderItem.Price = dish.Price;
                    if (dish.Promotion is { })
                    {
                        orderItem.Price = dish.Promotion.NewPrice;
                        orderItem.AppliedPriceOffer = dish.Promotion;
                    }

                    orderItem.ItemTotalSum = orderItem.Price * orderItem.Quantity;
                    totalSum += orderItem.ItemTotalSum;
                    orderItems.Add(orderItem);
                }

                order.TotalSum = totalSum;
                return orderItems;
            });


        var orders = ordersFaker.Generate(100);
        return new Data(products, weights, pieces, volumes, storages, storageProducts, fakeDishes, menus, ingredients,
            dishesWithIngredients, dishesInMenu, promotions, customers, contacts, addresses, employees, waiters,
            managers, orders, Enumerable.Empty<OrderItem>());
    }


    public record Data(IEnumerable<Product> Products, IEnumerable<UnitOfMeasure> Weights,
        IEnumerable<UnitOfMeasure> Pieces, IEnumerable<UnitOfMeasure> Volumes, IEnumerable<Storage> Storages,
        IEnumerable<StorageProduct> StorageProducts, IEnumerable<Dish> Dishes, IEnumerable<Menu> Menus,
        IEnumerable<Ingredient> Ingredients, IEnumerable<IngredientDish> DishesWithIngredients,
        IEnumerable<DishMenu> DishesInMenu,
        IEnumerable<PriceOffer> Promotions, IEnumerable<Customer> Customers, IEnumerable<Contact> Contacts,
        IEnumerable<Address> Addresses, IEnumerable<Employee> Employees, IEnumerable<Waiter> Waiters,
        IEnumerable<Manager> Managers, IEnumerable<Order> Orders, IEnumerable<OrderItem> OrderItems);
}


// var fruit = new[] { "apple", "banana", "orange", "strawberry", "kiwi" };
//
// var orderIds = 0;
// var testOrders = new Faker<Order>()
//     //Ensure all properties have rules. By default, StrictMode is false
//     //Set a global policy by using Faker.DefaultStrictMode
//     .StrictMode(true)
//     //OrderId is deterministic
//     .RuleFor(o => o.OrderId, f => orderIds++)
//     //Pick some fruit from a basket
//     .RuleFor(o => o.Item, f => f.PickRandom(fruit))
//     //A random quantity from 1 to 10
//     .RuleFor(o => o.Quantity, f => f.Random.Number(1, 10))
//     //A nullable int? with 80% probability of being null.
//     //The .OrNull extension is in the Bogus.Extensions namespace.
//     .RuleFor(o => o.LotNumber, f => f.Random.Int(0, 100).OrNull(f, .8f));
//
//
// var userIds = 0;
// var testUsers = new Faker<User>()
//     //Optional: Call for objects that have complex initialization
//     .CustomInstantiator(f => new User(userIds++, f.Random.Replace("###-##-####")))
//
//     //Use an enum outside scope.
//     .RuleFor(u => u.Gender, f => f.PickRandom<Gender>())
//
//     //Basic rules using built-in generators
//     .RuleFor(u => u.FirstName, (f, u) => f.Name.FirstName(u.Gender))
//     .RuleFor(u => u.LastName, (f, u) => f.Name.LastName(u.Gender))
//     .RuleFor(u => u.Avatar, f => f.Internet.Avatar())
//     .RuleFor(u => u.UserName, (f, u) => f.Internet.UserName(u.FirstName, u.LastName))
//     .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName))
//     .RuleFor(u => u.SomethingUnique, f => $"Value {f.UniqueIndex}")
//
//     //Use a method outside scope.
//     .RuleFor(u => u.CartId, f => Guid.NewGuid())
//     //Compound property with context, use the first/last name properties
//     .RuleFor(u => u.FullName, (f, u) => u.FirstName + " " + u.LastName)
//     //And composability of a complex collection.
//     .RuleFor(u => u.Orders, f => testOrders.Generate(3).ToList())
//     //Optional: After all rules are applied finish with the following action
//     .FinishWith((f, u) => { Console.WriteLine("User Created! Id={0}", u.Id); });
//
// var user = testUsers.Generate();
// Console.WriteLine(user.DumpAsJson());