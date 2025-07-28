CREATE TABLE combined_sensor_data (
    id SERIAL PRIMARY KEY,
    tray_id BIGINT NOT NULL,
    plant_type VARCHAR(100) NOT NULL,
    temperature REAL NOT NULL,
    target_temperature REAL NOT NULL,
    humidity REAL NOT NULL,
    target_humidity REAL NOT NULL,
    light REAL NOT NULL,
    target_light REAL NOT NULL,
    is_temperature_out_of_range BOOLEAN NOT NULL DEFAULT FALSE,
    is_humidity_out_of_range BOOLEAN NOT NULL DEFAULT FALSE,
    is_light_out_of_range BOOLEAN NOT NULL DEFAULT FALSE,
    timestamp TIMESTAMP WITHOUT TIME ZONE DEFAULT CURRENT_TIMESTAMP
);