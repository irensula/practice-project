/**
 * @param { import("knex").Knex } knex
 * @returns { Promise<void> } 
 */
const testPassword = "salasana"

var bcrypt = require('bcryptjs');
var salt = bcrypt.genSaltSync(10);
var hashedpassword = bcrypt.hashSync(testPassword, salt);

exports.seed = function(knex, Promise) {
  // Deletes ALL existing entries
  return knex('vocabulary').del()
    .then(function () {
      // Inserts seed entries
      return knex('vocabulary').insert([
        {  
          word: 'menu', 
          translation: 'ruokalista',
        },
        { 
          word: 'table', 
          translation: 'pöytä',
        },
      ]);
    });
};