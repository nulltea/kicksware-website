web-app:
	docker-compose down;
	docker-compose build;
	docker-compose up -d;

styles:
	mkdir -p Web/wwwroot/styles/css;
	for dir in $(shell ls Web/wwwroot/styles/less); do \
		printf "Building \e[1m$$dir\e[0m stylesheets:\n"; \
		mkdir -p Web/wwwroot/styles/css/$$dir; \
		lessc-each Web/wwwroot/styles/less/$$dir Web/wwwroot/styles/css/$$dir; \
	done;