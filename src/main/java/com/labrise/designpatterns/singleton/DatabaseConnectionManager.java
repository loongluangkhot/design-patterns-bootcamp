package com.labrise.designpatterns.singleton;

/**
 * DatabaseConnectionManager (scaffold)
 *
 * Goal: Implement a thread-safe Singleton that manages a database connection.
 *
 * Requirements to implement:
 * - Create a private constructor that accepts jdbcUrl, username, password
 * - Provide a thread-safe getInstance(...) (double-checked locking or enum or holder)
 * - Implement connect/disconnect/isConnected lifecycle
 * - Expose read-only getters for jdbcUrl and username (no password getter)
 */
public final class DatabaseConnectionManager {

    private static volatile DatabaseConnectionManager instance;

    private String jdbcUrl;
    private String username;
    private String password;
    private boolean isConnected;

    private DatabaseConnectionManager(String jdbcUrl, String username, String password) {
        this.jdbcUrl = jdbcUrl;
        this.username = username;
        this.password = password;
        this.isConnected = false;
    }

    /**
     * Return the singleton instance of the manager.
     * Choose a thread-safe approach and document it.
     */
    public static DatabaseConnectionManager getInstance(String jdbcUrl, String username, String password) {
        synchronized (DatabaseConnectionManager.class) {
            if (instance == null) {
                instance = new DatabaseConnectionManager(jdbcUrl, username, password);
            }
            return instance;
        }
    }

    /** Attempt to establish a connection. Return true if state changed to connected. */
    public boolean connect() {
        // Reference password to avoid unused-field warning and simulate a trivial check
        boolean credentialsOk = (password == null) || !password.isEmpty();
        this.isConnected = credentialsOk;
        return this.isConnected;
    }

    /** Attempt to close the connection. Return true if state changed to disconnected. */
    public boolean disconnect() {
        this.isConnected = false;
        return true;
    }

    /** Return current connection state. */
    public boolean isConnected() {
        return this.isConnected;
    }

    /** Getter for jdbcUrl. */
    public String getJdbcUrl() {
        return this.jdbcUrl;
    }

    /** Getter for username. */
    public String getUsername() {
        return this.username;
    }
}


