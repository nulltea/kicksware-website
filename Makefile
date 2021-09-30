include .env
export

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

build:
	docker build -f docker/Dockerfile . -t timothydockid/kicksware-web-app
	docker push timothydockid/kicksware-web-app

install:
	kubectl create secret generic google-oauth \
		--from-literal=AUTH_GOOGLE_ID=${AUTH_GOOGLE_ID} \
		--from-literal=AUTH_GOOGLE_SECRET=${AUTH_GOOGLE_SECRET} \
		--dry-run=client -o yaml | kubectl apply -f -
	kubectl create secret generic facebook-oauth \
		--from-literal=AUTH_FACEBOOK_ID=${AUTH_FACEBOOK_ID} \
		--from-literal=AUTH_FACEBOOK_SECRET=${AUTH_FACEBOOK_SECRET} \
		--dry-run=client -o yaml | kubectl apply -f -
	kubectl create secret generic github-oauth \
		--from-literal=AUTH_GITHUB_ID=${AUTH_GITHUB_ID} \
		--from-literal=AUTH_GITHUB_SECRET=${AUTH_GITHUB_SECRET} \
		--dry-run=client -o yaml | kubectl apply -f -
	kubectl create secret generic api-access \
		--from-literal=AUTH_ACCESS_KEY=${AUTH_ACCESS_KEY} \
		--dry-run=client -o yaml | kubectl apply -f -

	helm upgrade --install web-app ./webapp-chart

sync-source:
	rsync -r -v . --exclude .git ubuntu@${REMOTE_IP}:kicksware/web-app