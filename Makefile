web-app:
	docker-compose down;
	docker-compose build;
	docker-compose up -d;

styles:
	mkdir -p Web/wwwroot/styles/css2;
	for dir in $(shell ls Web/wwwroot/styles/less); do \
		printf "Building $$dir stylesheets:\n"; \
		mkdir -p Web/wwwroot/styles/css2/$$dir; \
		lessc-each Web/wwwroot/styles/less/$$dir Web/wwwroot/styles/css2/$$dir; \
	done;