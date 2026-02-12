exports.seed = async function(knex) {
  // Deletes ALL existing entries
  await knex('word_translations').del();
  // Inserts seed entries
  await knex('word_translations').insert([
    {  
      word_id: 1, 
      language_id: 1,
      text: 'ruoka',
      audio: 'audio/ruoka.mp3'
    },
    {  
      word_id: 1, 
      language_id: 2,
      text: 'food',
      audio: 'audio/food.mp3'
    },
    {  
      word_id: 2, 
      language_id: 1,
      text: 'ruokalista',
      audio: 'audio/ruokalista.mp3'
    },
    {  
      word_id: 2, 
      language_id: 2,
      text: 'menu',
      audio: 'audio/menu.mp3'
    },
    {  
      word_id: 3, 
      language_id: 1,
      text: 'pöytä',
      audio: 'audio/poyta.mp3'
    },
    {  
      word_id: 3, 
      language_id: 2,
      text: 'table',
      audio: 'audio/table.mp3'
    },
  ]);
};