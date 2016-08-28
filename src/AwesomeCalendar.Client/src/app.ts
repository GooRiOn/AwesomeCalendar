import {Router, RouterConfiguration} from 'aurelia-router';

export class App  
{  
    router: Router;

    configureRouter(config: RouterConfiguration, router: Router)
    {
        config.title = "Awesome Calendar";

        config.map([
          {route: ['', 'home'], name: 'home', moduleId: 'home', nav: true}
        ]);

        this.router = router;
    }  
}
