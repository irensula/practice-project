exports.seed = async function(knex) {
  // Deletes ALL existing entries
  await knex('words').del();
  // Inserts seed entries
  await knex('words').insert([
    {  
      base_language_id: 1, 
      base_word: 'ruoka',
      image: 'images/food.jpg'
    },
    {  
      base_language_id: 1, 
      base_word: 'ruokalista',
      image: 'images/menu.jpg'
    },
    { 
      base_language_id: 1, 
      base_word: 'pöytä',
      image: 'images/table.jpg'
    },
    { 
      base_language_id: 1, 
      base_word: 'tarjoilija',
      image: 'images/waiter.png'
    },
    { 
      base_language_id: 1, 
      base_word: 'asiakas',
      image: 'images/customer.jpg'
    },
    { 
      base_language_id: 1, 
      base_word: 'tilaus',
      image: 'images/order.jpg'
    },
    { 
      base_language_id: 1, 
      base_word: 'ravintola',
      image: 'images/restaurant.jpg'
    },
    { 
      base_language_id: 1, 
      base_word: 'kahvila',
      image: 'images/coffee_shop.jpg'
    },
    { 
      base_language_id: 1, 
      base_word: 'tarjotin',
      image: 'images/tray.jpg'
    },
    { 
      base_language_id: 1, 
      base_word: 'lautanen',
      image: 'images/plate.png'
    },
  ]);
};