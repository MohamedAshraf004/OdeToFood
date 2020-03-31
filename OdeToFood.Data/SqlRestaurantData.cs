using Microsoft.EntityFrameworkCore;
using OdeToFood.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OdeToFood.Data
{
    public class SqlRestaurantData : IRestaurantData
    {
        private readonly OdeToFoodDbContext dbContext;

        public SqlRestaurantData(OdeToFoodDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public int Commit()
        {
            return dbContext.SaveChanges();
        }

        public Restaurant Create(Restaurant newRestaurant)
        {
            dbContext.Add(newRestaurant);
            return newRestaurant;
        }

        public Restaurant Delete(int restaurantId)
        {
            var restaurant = GetById(restaurantId);
            if (restaurant !=null)
            {
                dbContext.Restaurants.Remove(restaurant);
            }
            return restaurant;
        }

        public Restaurant GetById(int id)
        {
            return dbContext.Restaurants.Find(id);

        }

        public IEnumerable<Restaurant> GetRestaurantsByName(string name)
        {
          
                return dbContext.Restaurants.Where(n => n.Name.Contains(name)).OrderBy(n => n.Name);
            
        }

        public Restaurant Update(Restaurant updatedRestaurant)
        {
            var restaurant = dbContext.Restaurants.Attach(updatedRestaurant);
            restaurant.State = EntityState.Modified;
            return updatedRestaurant;
        }
    }
}
