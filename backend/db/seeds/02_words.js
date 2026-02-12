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
  ]);
};