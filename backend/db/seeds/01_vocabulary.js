/**
 * @param { import("knex").Knex } knex
 * @returns { Promise<void> } 
 */
exports.seed = function(knex, Promise) {
  // Deletes ALL existing entries
  return knex('vocabulary').del()
    .then(function () {
      // Inserts seed entries
      return knex('vocabulary').insert([
        {  
          word: 'ruoka', 
          translation: 'food',
        },
        {  
          word: 'ruokalista', 
          translation: 'menu',
        },
        { 
          word: 'pöytä', 
          translation: 'table',
        },
      ]);
    });
};