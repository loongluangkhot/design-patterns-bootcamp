package com.labrise.designpatterns.singleton;

import java.util.Properties;

/**
 * AppConfig (scaffold)
 *
 * Goal: Provide a global application configuration as a Singleton.
 *
 * Requirements to implement:
 * - Choose a singleton style (eager, lazy, enum, holder pattern)
 * - Maintain an internal Properties map of values
 * - Provide get(key), getOrDefault(key, defaultValue), and set(key, value)
 * - Optionally load defaults from environment or application.properties
 */
public final class AppConfig {

    private static volatile AppConfig instance;

    private Properties properties;

    private AppConfig() {
        this.properties = new Properties();
    }

    public static AppConfig getInstance() {
        synchronized (AppConfig.class) {
            if (instance == null) {
                instance = new AppConfig();
            }
            return instance;
        }
    }

    public String get(String key) {
        return properties.getProperty(key);
    }

    public String getOrDefault(String key, String defaultValue) {
        return properties.getProperty(key, defaultValue);
    }

    public void set(String key, String value) {
        properties.setProperty(key, value);
    }
}


