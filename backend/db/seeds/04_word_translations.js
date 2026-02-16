exports.seed = async function(knex) {
  // Deletes ALL existing entries
  await knex('word_translations').del();
  // Inserts seed entries
  await knex('word_translations').insert([
    // 1 — ruoka
    { word_id: 1, language_id: 1, text: 'ruoka',        audio: 'audio/fi/ruoka.mp3' },
    { word_id: 1, language_id: 2, text: 'food',         audio: 'audio/en/food.mp3' },

    // 2 — ruokalista
    { word_id: 2, language_id: 1, text: 'ruokalista',   audio: 'audio/fi/ruokalista.mp3' },
    { word_id: 2, language_id: 2, text: 'menu',         audio: 'audio/en/menu.mp3' },

    // 3 — pöytä
    { word_id: 3, language_id: 1, text: 'pöytä',        audio: 'audio/fi/poyta.mp3' },
    { word_id: 3, language_id: 2, text: 'table',        audio: 'audio/en/table.mp3' },

    // 4 — tarjoilija
    { word_id: 4, language_id: 1, text: 'tarjoilija',   audio: 'audio/fi/tarjoilija.mp3' },
    { word_id: 4, language_id: 2, text: 'waiter',       audio: 'audio/en/waiter.mp3' },

    // 5 — asiakas
    { word_id: 5, language_id: 1, text: 'asiakas',      audio: 'audio/fi/asiakas.mp3' },
    { word_id: 5, language_id: 2, text: 'customer',     audio: 'audio/en/customer.mp3' },

    // 6 — tilaus
    { word_id: 6, language_id: 1, text: 'tilaus',       audio: 'audio/fi/tilaus.mp3' },
    { word_id: 6, language_id: 2, text: 'order',        audio: 'audio/en/order.mp3' },

    // 7 — ravintola
    { word_id: 7, language_id: 1, text: 'ravintola',    audio: 'audio/fi/ravintola.mp3' },
    { word_id: 7, language_id: 2, text: 'restaurant',   audio: 'audio/en/restaurant.mp3' },

    // 8 — kahvila
    { word_id: 8, language_id: 1, text: 'kahvila',      audio: 'audio/fi/kahvila.mp3' },
    { word_id: 8, language_id: 2, text: 'coffee shop',  audio: 'audio/en/coffee_shop.mp3' },

    // 9 — tarjotin
    { word_id: 9, language_id: 1, text: 'tarjotin',  audio: 'audio/fi/tarjotin.mp3' },
    { word_id: 9, language_id: 2, text: 'tray',      audio: 'audio/en/tray.mp3' },

    // 10 — lautanen
    { word_id: 10, language_id: 1, text: 'lautanen', audio: 'audio/fi/lautanen.mp3' },
    { word_id: 10, language_id: 2, text: 'plate',    audio: 'audio/en/plate.mp3' },
  ]);
};