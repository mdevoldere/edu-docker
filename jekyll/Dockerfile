FROM jekyll/jekyll:3

# COPY Gemfile /srv/jekyll/

WORKDIR /srv/jekyll

RUN apk update && \
	apk add ruby-dev gcc make curl build-base libc-dev libffi-dev zlib-dev libxml2-dev libgcrypt-dev libxslt-dev

RUN gem install jekyll 
# -v 3.9.3
RUN gem install bundler -v 2.4.22
RUN gem update --system 3.2.3 

# RUN bundle install

EXPOSE 4000 35729
