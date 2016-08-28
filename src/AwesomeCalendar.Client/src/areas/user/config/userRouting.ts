import {Router, RouterConfiguration} from 'aurelia-router';

export class UserRouting
{
    router: Router;
    heading: 'User';

    configureRouter(config: RouterConfiguration, router: Router)
    {
        this.router = router;
        config.title = this.heading;

        config.map([
            {route: 'login', name: 'login', moduleId: '../viewModels/login', nav:true, title: 'Login'},
            {route: 'register', name: 'register', moduleId: '../viewModels/register', nav:true, title: 'Register'}
        ]);
    }
}