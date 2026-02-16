/**
 * @param { import("knex").Knex } knex
 * @returns { Promise<void> }
 */
exports.up = function(knex) {
    return knex.schema
      .createTable('languages', t => {
          t.increments('id').primary();
          t.string('lang_code').notNullable().unique();
          t.string('lang_name').notNullable();
      })
      .createTable('users', t => {
          t.increments('id').primary()
          t.string('useremail').notNullable().unique();
          t.string('password').notNullable()
      })
      .createTable('words', t => {
          t.increments('id').primary();
          t.integer('base_language_id')
            .unsigned()
            .notNullable()
            .references('id')
            .inTable('languages')
            .onDelete('CASCADE');
          t.string('base_word').notNullable();
          t.string('image');
      })
      .createTable('word_translations', t => {
          t.increments('id').primary();
          t.integer('word_id')
            .unsigned()
            .notNullable()
            .references('id')
            .inTable('words')
            .onDelete('CASCADE');
          t.integer('language_id')
            .unsigned()
            .notNullable()
            .references('id')
            .inTable('languages')
            .onDelete('CASCADE');
          t.string('text').notNullable();
          t.string('audio');

          t.unique(['word_id', 'language_id']);
      })
    };

/**
 * @param { import("knex").Knex } knex
 * @returns { Promise<void> }
 */
// child tables first before parent tables
exports.down = function(knex) {
  return knex.schema
    .dropTableIfExists('word_translations')
    .dropTableIfExists('words')
    .dropTableIfExists('users')
    .dropTableIfExists('languages')
  };