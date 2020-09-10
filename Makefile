web-app:
	docker-compose down;
	docker-compose build;
	docker-compose up -d;

styles:
	mkdir -p Web/wwwroot/styles/css2;
	for dir in $(shell ls Web/wwwroot/styles/less); do \
		mkdir -p Web/wwwroot/styles/css/$$dir; \
		lessc-each Web/wwwroot/styles/less/$$dir Web/wwwroot/styles/css/$$dir; \
	done;