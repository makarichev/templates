import App from './App.svelte';
import Home from './Components/Home.svelte';
import About from './Components/About.svelte';

import {route} from './Shared/Store';

import page from 'page';


const app = new App({
	target: document.body,
	props: {
		name: 'Svelte Template'
	}
});

let _setRoute = (ctx, comp) => {
	route.set(ctx);
	app.$set({component: comp})
}


page('/', x => _setRoute(x, Home))
page('/about', x => _setRoute(x, About))

page.start();




export default app;