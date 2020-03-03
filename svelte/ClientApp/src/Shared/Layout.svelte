<script>
  import {getContext} from 'svelte'
  import Filter from './Filter.svelte'
  import {user} from './Store.js'

  let name = getContext('name');


let actionSlot;
  $: showActions = actionSlot && actionSlot.innerHTML !== "";


let filterShow = false;
export let filterApply;


let showUser = false;
let loginUrl = `${window.location.protocol}//${window.location.hostname}/login?ReturnUrl=${window.location.href}`;

</script>

<style>
  .hidden {
    display: none;
  }

  .starter-template {
    padding: 0.5rem;
  }

  .i-userbar__user {
    width: 40px;
    height: 40px;
    overflow: hidden;
    border: 1px solid #ddd;
    cursor: pointer;
    right: -35px;
}
</style>

<nav class="navbar navbar-expand-md navbar-light bg-light fixed-top">
  <a class="navbar-brand" href="/">{name}</a>
  <button
    class="navbar-toggler"
    type="button"
    data-toggle="collapse"
    data-target="/navbarsExampleDefault"
    aria-controls="navbarsExampleDefault"
    aria-expanded="false"
    aria-label="Toggle navigation">
    <span class="navbar-toggler-icon" />
  </button>

  <div class="collapse navbar-collapse" id="navbarsExampleDefault">
    <ul class="navbar-nav mr-auto">
      <li class="nav-item active">
        <a class="nav-link" href="/">
          Home
          <span class="sr-only">(current)</span>
        </a>
      </li>
      <li class="nav-item">
        <a class="nav-link" href="/about">About</a>
      </li>
      <li class="nav-item">
        <a class="nav-link disabled" href="/">Disabled</a>
      </li>

      <li class="nav-item dropdown" class:hidden={!showActions}>
        <a
          class="nav-link dropdown-toggle"
          href="http://example.com"
          id="dropdown01"
          data-toggle="dropdown"
          aria-haspopup="true"
          aria-expanded="false">
          Действия
        </a>
        <div class="dropdown-menu" bind:this={actionSlot}>
          <slot name="actions" />
        </div>
      </li>
    </ul>
    <form class="form-inline my-2 my-lg-0">

      <div class="input-group">
        <input
          class="form-control"
          type="text"
          placeholder="Поиск"
          aria-label="Search" />
        <div class="input-group-append" id="button-addon4">
          <button class="btn btn-outline-secondary" type="search">
            <i class="fa fa-search">
          </button>
          <button class="btn btn-outline-secondary" type="button" on:click={x => filterShow = true }>
            <i class="fa fa-filter">
          </button>


        </div>

        
      </div>

    </form>


    <a href={loginUrl} class="ml-4">
    {#if $user.name} 
      <img src="https://cisp.ssnab.ru/Files/MolsPhotos/w80/{$user.mol_id}.jpg" alt="{$user.name}" width="30" height="38" title="{$user.name}">
      {:else}
      <i class="text-muted fa fa-question-circle fa-2x"></i>
      {/if}
    </div>

</nav>

<main role="main">



  <div class="starter-template">
    <slot />
  </div>

</main>
<!-- /.container -->



<Filter bind:show={filterShow} bind:apply={filterApply}>
  <div>
    <slot name="filter"></slot>
  </div>
</Filter>


