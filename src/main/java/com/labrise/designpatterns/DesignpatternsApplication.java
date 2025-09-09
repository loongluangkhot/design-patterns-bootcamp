package com.labrise.designpatterns;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import com.labrise.designpatterns.singleton.AppConfig;
import com.labrise.designpatterns.singleton.DatabaseConnectionManager;

@SpringBootApplication
public class DesignpatternsApplication {

	public static void main(String[] args) {
		SpringApplication.run(DesignpatternsApplication.class, args);

		AppConfig config = AppConfig.getInstance();
		DatabaseConnectionManager manager = DatabaseConnectionManager.getInstance(config.get("db.url"), config.get("db.username"), config.get("db.password"));
		manager.connect();
		System.out.println("Database connected? " + manager.isConnected());
		manager.disconnect();
		System.out.println("Database disconnected? " + manager.isConnected());
	}

}
