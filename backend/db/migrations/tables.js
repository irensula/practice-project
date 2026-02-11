/**
 * @param { import("knex").Knex } knex
 * @returns { Promise<void> }
 */
exports.up = function(knex) {
    return knex.schema
    
    .createTable('vocabulary', t => {
        t.increments('id').primary()
        t.string('word').notNullable()
        t.string('translation').notNullable()
    })    
  };

/**
 * @param { import("knex").Knex } knex
 * @returns { Promise<void> }
 */
// child tables first before parent tables
exports.down = function(knex) {
return knex.schema
.dropTableIfExists('vocabulary')
};