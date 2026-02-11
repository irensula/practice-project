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
  return knex('users').del()
    .then(function () {
      // Inserts seed entries
      return knex('users').insert([
        {  
          useremail: 'testuser@gmail.com', 
          password: hashedpassword,
        }
      ]);
    });
};