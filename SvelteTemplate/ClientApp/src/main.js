import App from './App.svelte';
import Home from './Components/Home.svelte';
import About from './Components/About.svelte';
 import Mols from './Components/Mols/Mols.svelte';
 import Mol from './Components/Mols/Mol.svelte';
 import New from './Components/Mols/New.svelte';

import {route} from './store.js';

import page from 'page';


const app = new App({
	target: document.body,
	props: {
		name: 'Svelte Template',
		component: null
	}
});

let _setRoute = (ctx, comp) => {
	route.set(ctx);
	app.$set({component: comp})
}


page('/', x => _setRoute(x, Home))
page('/about', x => _setRoute(x, About))
page('/mols', x => _setRoute(x, Mols))
page('/mols/new', x => _setRoute(x, New))
page('/mols/:id', x => _setRoute(x, Mol))


page.start();




export default app;