exports.seed = async function(knex) {
  // Deletes ALL existing entries
  await knex('languages').del();
  
  // Inserts seed entries
  await knex('languages').insert([
    {  
      lang_code: 'fi', 
      lang_name: 'Suomi',
    },
    {  
      lang_code: 'en-GB', 
      lang_name: 'British English',
    }
  ]);
};