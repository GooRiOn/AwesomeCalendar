import {Router, RouterConfiguration} from 'aurelia-router';

export class App  
{  
    router: Router;

    configureRouter(config: RouterConfiguration, router: Router)
    {
        this.router = router;
        config.title = "Awesome Calendar";

        config.map([
          {route: ['', 'home'], name: 'home', moduleId: 'home', nav: true},
          { route: 'user', name: 'user', moduleId: 'areas/user/config/userRouting', nav: true, title: 'User' }
        ]);        
    }  
}
