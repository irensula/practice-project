const testPassword = "salasana"

var bcrypt = require('bcryptjs');
var salt = bcrypt.genSaltSync(10);
var hashedpassword = bcrypt.hashSync(testPassword, salt);

exports.seed = async function(knex) {
  // Deletes ALL existing entries
  await knex('users').del();
  // Inserts seed entries
  await knex('users').insert([
    {  
      useremail: 'testuser@gmail.com', 
      password: hashedpassword,
    }
  ]);
};