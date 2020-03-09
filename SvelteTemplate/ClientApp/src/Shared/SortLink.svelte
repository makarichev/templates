<script>
    import { createEventDispatcher } from 'svelte';
    
    const dispatch = createEventDispatcher();

    export let name = null;
    
    export let sort = null;

    function onSorted() {
        dispatch('sorted', `${name} ${asc}`);
    }
    
    let sortname = name, asc = "ASC"

    $: {
        let current = sort.split(' ')
        sortname = current[0];
        if (current.length > 1) asc = current[1];
    }

</script>

<style>
    a {white-space: nowrap}
</style>

<a href="/" on:click|preventDefault={onSorted}>
    <slot></slot>
    <!-- {#if sortname === name} 
        {#if asc === 'DESC'}
        <i class="fa fa-chevron-up"></i>
        {:else}
        <i class="fa fa-chevron-down"></i>
        {/if}
    {/if} -->
    {asc}
</a>


