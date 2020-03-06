<script>
  import { onMount, onDestroy, setContext } from "svelte";
  
  

  
	export let name;
	export let context;
	export let component;

	setContext('name', name);

	


</script>



<svelte:head>
  <title>{name}</title>
  <base href="/"><base>
</svelte:head>

{#if component}
	<svelte:component this={component} route={context} {name}></svelte:component>
{/if}
