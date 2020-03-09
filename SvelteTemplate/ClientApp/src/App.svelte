<script>
  import { onMount, onDestroy, setContext } from "svelte";
  import {user, reglament, route} from './store.js'
  
	let s1 = user.subscribe(x => {})
	let s2 = reglament.subscribe(x => {})
	onDestroy(s1)
	onDestroy(s2)


  
export let name;
export let component;

setContext('name', name);

	


</script>



<svelte:head>
  <title>{name}</title>
</svelte:head>

{#if component}
	<svelte:component this={component}></svelte:component>
{/if}

