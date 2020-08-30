web-app:
	docker-compose down;
	docker-compose build;
	docker-compose push web-app;
	docker-compose up -d;

styles:
	mkdir web-app-service/Web/wwwroot/styles/css;
	for dir in web-app-service/Web/wwwroot/styles/less/*; do \
		lessc-each web-app-service/Web/wwwroot/styles/less/$(basename ${dir}) web-app-service/Web/wwwroot/styles/css/$(basename ${dir}); \
	done;